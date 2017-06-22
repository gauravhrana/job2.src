IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='PersonTitleSearch')
BEGIN
	PRINT 'Dropping Procedure PersonTitleSearch'
	DROP Procedure PersonTitleSearch
END
GO

PRINT 'Creating Procedure PersonTitleSearch'
GO

/******************************************************************************
**		File: 
**		Name: PersonTitleSearch
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
			EXEC PersonTitleSearch NULL	, NULL	, NULL
			EXEC PersonTitleSearch NULL	, 'K'	, NULL
			EXEC PersonTitleSearch 1	, 'K'	, NULL
			EXEC PersonTitleSearch 1	, NULL	, NULL
			EXEC PersonTitleSearch NULL	, NULL	, 'W'

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
Create procedure dbo.PersonTitleSearch
(
		@PersonTitleId			INT				= NULL 	
	,	@Name					VARCHAR(50)		= NULL 	
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'PersonTitle'
)
AS
BEGIN
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'Name' 
	SET @InputValuesLocal			= @Name
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.PersonTitleSearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId	AS INT
	EXEC	dbo.SystemEntityTypeGetId 
			@SystemEntityType	=	@SystemEntityType
		,	@SystemEntityTypeId =	@SystemEntityTypeId OUTPUT


	-- if the PersonTitle did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END

	SELECT	a.*
	FROM	dbo.PersonTitle a
	WHERE	a.Name LIKE @Name	+ '%'
	AND		a.PersonTitleId =
			CASE
				WHEN @PersonTitleId IS NULL THEN a.PersonTitleId
				ELSE @PersonTitleId
			END	
	ORDER BY a.SortOrder	ASC
		,	 a.Name			ASC
		,	 a.PersonTitleId	ASC


	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @PersonTitleId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
	

