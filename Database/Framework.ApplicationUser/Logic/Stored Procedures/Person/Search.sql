IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonSearch')
BEGIN
	PRINT 'Dropping Procedure PersonSearch'
	DROP  Procedure  PersonSearch
END
GO

PRINT 'Creating Procedure PersonSearch'
GO

/******************************************************************************
**		File: 
**		Name: PersonSearch
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
			EXEC PersonSearch NULL	, NULL	, NULL
			EXEC PersonSearch NULL	, 'K'	, NULL
			EXEC PersonSearch 1		, 'K'	, NULL
			EXEC PersonSearch 1		, NULL	, NULL
			EXEC PersonSearch NULL	, NULL	, 'W'

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
CREATE Procedure dbo.PersonSearch
(
		@PersonId			INT				= NULL	
	,	@NameLast			VARCHAR(50)		= NULL	
	,	@NameFirst			VARCHAR(50)		= NULL	
	,	@NameMiddle			VARCHAR(50)		= NULL  
	,	@PersonTitleId		INT				= NULL	
	,	@AuditId			INT						
	,	@AuditDate			DATETIME		= NULL		
	,	@SystemEntityType	VARCHAR(50)		= 'Person'
)
AS
BEGIN
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'FirstName' + ', '+ 'LastName'
	SET @InputValuesLocal			= @NameFirst + ', ' + @NameLast
	EXEC dbo.StoredProcedureLogInsert
			@Name					= 'dbo.PersonSearch'
		,	@InputParameters		= @InputParametersLocal
		,	@InputValues			= @InputValuesLocal


	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId	AS INT
	EXEC	dbo.SystemEntityTypeGetId 
			@SystemEntityType	=	@SystemEntityType
		,	@SystemEntityTypeId =	@SystemEntityTypeId OUTPUT

	-- if the client did not provide any values
	-- assume search on all possiblities ('%')
	SET	@NameFirst	= ISNULL(@NameFirst, '%')					
	SET	@NameLast	= ISNULL(@NameLast, '%')
	SET	@NameMiddle	= ISNULL(@NameMiddle, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(LTRIM(RTRIM(@NameFirst))) = 0 
	BEGIN
		SET	@NameFirst = '%'
	END

	IF LEN(LTRIM(RTRIM(@NameLast))) = 0 
	BEGIN
		SET	@NameLast = '%'
	END

	IF LEN(LTRIM(RTRIM(@NameMiddle))) = 0 
	BEGIN
		SET	@NameMiddle = '%'
	END
	
	--Here only the first charecter of the string is required to search	
	SELECT	a.PersonId										
		,	a.FirstName										
		,	a.LastName	
		,	a.MiddleName
		,	a.PersonTitleId	
		,	b.Name													AS 'PersonTitle'								
		,	a.FirstName	+ ' ' + a.LastName +	' ' + a.LastName	AS 'FullName'			
	FROM		Person		a	
	INNER JOIN 	PersonTitle	b	ON a.PersonTitleId = b.PersonTitleId

	WHERE	a.LastName	LIKE @NameLast	+ '%'
	AND		a.FirstName LIKE @NameFirst	+ '%'
	AND		a.MiddleName LIKE @NameMiddle + '%'
	AND		a.PersonTitleId =
			CASE 
				WHEN @PersonTitleId IS NULL THEN a.PersonTitleId
				ELSE @PersonTitleId
			END
	AND		a.PersonID =
			CASE 
				WHEN @PersonId IS NULL THEN a.PersonId
				ELSE @PersonId
			END	
	ORDER BY	a.LastName		ASC 
		,		a.FirstName		ASC
		,	    a.MiddleName	ASC
		,		a.PersonId		ASC
		
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	 
		,	@EntityKey				= @PersonId
		,	@AuditAction			= 'Search' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO

