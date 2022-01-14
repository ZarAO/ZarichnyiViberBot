USE [Track]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WalkInfo](
	[id] [uniqueidentifier] DEFAULT NEWID() NOT NULL,
	[IMEI] [varchar](50) NOT NULL,
	[Distance] [decimal](12, 9) NOT NULL,
	[DateWalk] [Date] NOT NULL,
	[TimeWalk] [time] NOT NULL,
	[WalkNumber] [int],
 CONSTRAINT [PK_WalkInfo_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE OR ALTER PROCEDURE sp_GetWalkPointInfo
AS
	BEGIN
		DECLARE @IMEI [varchar](50);
		DECLARE @ID uniqueidentifier;
		DECLARE @Old_ID uniqueidentifier;
		DECLARE @WalkNumber int;
		DECLARE @PointNumberInWalk int = 1;

		WHILE EXISTS(SELECT IMEI FROM #TrackLocationTemp WHERE WalkNumber IS NULL)
		BEGIN

			SET @WalkNumber = 1

			SELECT 
				TOP 1 
				@IMEI = IMEI,
				@ID = Id 
			FROM #TrackLocationTemp 
			WHERE WalkNumber IS NULL 
			ORDER BY date_track ASC

			IF EXISTS (SELECT TOP 1 IMEI FROM #TrackLocationTemp WHERE IMEI = @IMEI AND WalkNumber IS NULL)
				BEGIN
					WHILE EXISTS(SELECT TOP 1 id FROM #TrackLocationTemp WHERE id = @ID)
					BEGIN
						UPDATE #TrackLocationTemp SET WalkNumber = @WalkNumber, PointNumberInWalk = @PointNumberInWalk WHERE id = @ID

						SET @Old_ID = @ID

						SELECT 
							TOP 1 
							@ID = Id 
						FROM #TrackLocationTemp 
						WHERE WalkNumber IS NULL
						AND IMEI = @IMEI
						ORDER BY date_track ASC

						IF (@ID <> @Old_ID)
							BEGIN
								IF DATEDIFF(
											MINUTE, 
											(SELECT date_track FROM #TrackLocationTemp WHERE id = @Old_ID), 
											(SELECT date_track FROM #TrackLocationTemp WHERE id = @ID)) > 30
									BEGIN
										SET @WalkNumber += 1
										SET @PointNumberInWalk = 1
									END
								ELSE 
									BEGIN
										SET @PointNumberInWalk += 1
									END
								CONTINUE
							END
						ELSE
							BEGIN
								SET @PointNumberInWalk = 1
								BREAK
							END
					END
					CONTINUE
				END
			ELSE
				BEGIN
					BREAK
				END
		END	
	END


CREATE OR ALTER PROCEDURE sp_GetWalkInfo
AS
	BEGIN
		TRUNCATE TABLE WalkInfo
		CREATE TABLE #TrackLocationTemp(
			[id] [uniqueidentifier] DEFAULT NEWID() NOT NULL,
			[TL_Id] [int] NOT NULL,
			[IMEI] [varchar](50) NOT NULL,
			[latitude] [decimal](12, 9) NOT NULL,
			[longitude] [decimal](12, 9) NOT NULL,
			[date_track] [datetime] NOT NULL,
			[TypeSource] [int] NOT NULL,
			[WalkNumber] [int],
			[PointNumberInWalk] [int])

		INSERT INTO #TrackLocationTemp ([TL_Id],
			[IMEI],
			[latitude],
			[longitude],
			[date_track],
			[TypeSource]) SELECT [id],
			[IMEI],
			[latitude],
			[longitude],
			[date_track],
			[TypeSource] FROM [TrackLocation] WITH(NOLOCK)

		EXEC sp_GetWalkPointInfo

		DECLARE @IMEI [varchar](50) = '359339077003915';
		DECLARE @ID uniqueidentifier;
		DECLARE @Old_ID uniqueidentifier;
		DECLARE @WalkNumber int = 1;
		DECLARE @PointNumber int = 1;
		DECLARE @Distance float = 0;

		WHILE EXISTS(SELECT id FROM #TrackLocationTemp WHERE WalkNumber = @WalkNumber AND IMEI = @IMEI)
		BEGIN

			SELECT 
				TOP 1 
				@Old_ID = Id
			FROM #TrackLocationTemp 
			WHERE WalkNumber = @WalkNumber
			AND IMEI = @IMEI
			AND PointNumberInWalk = @PointNumber
			ORDER BY date_track ASC
			IF EXISTS (SELECT TOP 1 id FROM #TrackLocationTemp WHERE id = @Old_ID AND WalkNumber = @WalkNumber AND IMEI = @IMEI AND PointNumberInWalk = @PointNumber)
				BEGIN
					SELECT 
						TOP 1 
						@ID = Id
					FROM #TrackLocationTemp 
					WHERE WalkNumber = @WalkNumber
					AND IMEI = @IMEI
					AND PointNumberInWalk = @PointNumber + 1
					ORDER BY date_track ASC

					IF EXISTS (SELECT TOP 1 id FROM #TrackLocationTemp WHERE id = @ID AND WalkNumber = @WalkNumber AND IMEI = @IMEI AND PointNumberInWalk = @PointNumber + 1)
						BEGIN
							SET @Distance += geography::Point(
																(SELECT latitude FROM #TrackLocationTemp WHERE id = @Old_ID), 
																(SELECT longitude FROM #TrackLocationTemp WHERE id = @Old_ID), 
																4326
															).STDistance(
																			geography::Point(
																								(SELECT latitude FROM #TrackLocationTemp WHERE id = @ID), 
																								(SELECT longitude FROM #TrackLocationTemp WHERE id = @ID), 
																								4326
																							)
																		)/1000
							SET @PointNumber += 1
							CONTINUE
						END
					ELSE
						BEGIN
							DECLARE @StartDate datetime = (select date_track from #TrackLocationTemp where WalkNumber = @WalkNumber AND IMEI = @IMEI AND PointNumberInWalk = 1)
							DECLARE @EndDate datetime = (select date_track from #TrackLocationTemp where WalkNumber = @WalkNumber AND IMEI = @IMEI AND PointNumberInWalk = @PointNumber)
							IF (@Distance = 0)
							BEGIN
								SET @WalkNumber += 1
								SET @PointNumber = 1
								SET @Distance = 0
								IF EXISTS(SELECT id FROM #TrackLocationTemp WHERE WalkNumber = @WalkNumber AND IMEI = @IMEI)
									BEGIN
										CONTINUE
									END
								ELSE
									BEGIN
										SELECT TOP 1 @IMEI = IMEI FROM #TrackLocationTemp WHERE IMEI <> @IMEI AND IMEI NOT IN (SELECT DISTINCT IMEI FROM [dbo].[WalkInfo])
										IF EXISTS (SELECT DISTINCT IMEI FROM [dbo].[WalkInfo] WHERE IMEI = @IMEI)
										BEGIN 
											BREAK
										END
										SET @WalkNumber = 1
										CONTINUE
									END
							END
							INSERT INTO WalkInfo (
								[IMEI],
								[Distance],
								[DateWalk],
								[TimeWalk],
								[WalkNumber]
							) VALUES (
								@IMEI,
								@Distance,
								CAST(@EndDate as date),
								CAST((@EndDate - @StartDate) as time(0)),
								@WalkNumber
							)

							SET @WalkNumber += 1
							SET @PointNumber = 1
							SET @Distance = 0

							IF EXISTS(SELECT id FROM #TrackLocationTemp WHERE WalkNumber = @WalkNumber AND IMEI = @IMEI)
								BEGIN
									CONTINUE
								END
							ELSE
								BEGIN
									SELECT TOP 1 @IMEI = IMEI FROM #TrackLocationTemp WHERE IMEI <> @IMEI AND IMEI NOT IN (SELECT DISTINCT IMEI FROM [dbo].[WalkInfo])
									IF EXISTS (SELECT DISTINCT IMEI FROM [dbo].[WalkInfo] WHERE IMEI = @IMEI)
									BEGIN 
										BREAK
									END
									SET @WalkNumber = 1
									CONTINUE
								END
						END
				END
		END

		DROP TABLE #TrackLocationTemp
	END


EXEC sp_GetWalkInfo