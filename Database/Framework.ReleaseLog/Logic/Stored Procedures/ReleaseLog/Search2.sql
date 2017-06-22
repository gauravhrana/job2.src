IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ReleaseLogSearch2')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogSearch2'
	DROP Procedure ReleaseLogSearch2
END
GO

PRINT 'Creating Procedure ReleaseLogSearch2'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseLogSearch2
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
**		Sample:   
**              
			EXEC ReleaseLogSearch2 NULL	, NULL	, NULL
			EXEC ReleaseLogSearch2 NULL	, 'K'	, NULL
			EXEC ReleaseLogSearch2 1		, 'K'	, NULL
			EXEC ReleaseLogSearch2 1		, NULL	, NULL
			EXEC ReleaseLogSearch2 NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE procedure dbo.ReleaseLogSearch2
(
		@ReleaseLogId			INT				= NULL 		
	,	@ApplicationId			INT				= NULL 	
	,	@ReleaseLogStatusId			INT				= NULL 		
	,	@Name					VARCHAR(50)		= NULL 	
	,	@AuditId				INT		
	,	@ReleaseDateMin			DATETIME		= NULL	
	,	@ReleaseDateMax			DATETIME		= NULL	
	,	@AuditDate				DATETIME		= NULL	
	,	@ShowResults			INT				= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'ReleaseLog'	
	,	@ApplicationMode		INT				= NULL		
	,	@AddAuditInfo			INT				 = 1
	,	@AddTraceInfo			INT				 = 0
	,	@ReturnAuditInfo		INT				 = 0	 
)
WITH RECOMPILE
AS
BEGIN

	SET	NOCOUNT ON

	IF @AddTraceInfo = 1 
		BEGIN

			DECLARE @InputParametersLocal	VARCHAR(500)  
			DECLARE @InputValuesLocal		VARCHAR(5000)  
			SET @InputParametersLocal		= 'Name' 
			SET @InputValuesLocal			= @Name
			EXEC dbo.StoredProcedureLogInsert
					@Name						= 'dbo.ReleaseLogSearch2'
				,	@InputParameters			= @InputParametersLocal
				,	@InputValues				= @InputValuesLocal
		
		END	

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	
	-- if the ReleaseLog did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END

	IF object_id('tempdb..#ReleaseLogSearch_Result') IS NULL

	BEGIN
   	CREATE table #ReleaseLogSearch_Result
	(
		ReleaseLogId       INT NOT NULL,
		ApplicationId      INT NOT NULL,
		ReleaseLogStatusId INT NOT NULL,
		Name               VARCHAR (50) NOT NULL,
		VersionNo          VARCHAR (50) NOT NULL,
		ReleaseDate        DATETIME NOT NULL,
		Description        VARCHAR (50) NOT NULL,
		SortOrder          INT NOT NULL,
	
	)

	END

INSERT INTO	#ReleaseLogSearch_Result
	SELECT	a.*	
	FROM	dbo.ReleaseLog a
	WHERE	a.Name LIKE @Name	+ '%'
	AND a.ApplicationId	= ISNULL(@ApplicationId, a.ApplicationId)
	AND a.ReleaseLogStatusId	= ISNULL(@ReleaseLogStatusId, a.ReleaseLogStatusId)
	AND a.ReleaseLogId	= ISNULL(@ReleaseLogId, a.ReleaseLogId)
	AND a.ReleaseDate  >= ISNULL(@ReleaseDateMin, a.ReleaseDate)
	AND a.ReleaseDate  <= ISNULL(@ReleaseDateMax, a.ReleaseDate)
	ORDER BY SortOrder			ASC
		,	 ReleaseLogId		ASC

	IF @ShowResults = 1		
	BEGIN 
	SELECT	ReleaseLogId, 
			ApplicationId, 
			Name, 
			Description, 
			SortOrder 
	FROM #ReleaseLogSearch_Result
	END

END
GO

