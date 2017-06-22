IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='StoredProcedureLogSearch')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogSearch'
	DROP Procedure StoredProcedureLogSearch
END
GO

PRINT 'Creating Procedure StoredProcedureLogSearch'
GO

/******************************************************************************
**		File: 
**		Name: StoredProcedureLogSearch
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
			EXEC StoredProcedureLogSearch NULL	, NULL	, NULL
			EXEC StoredProcedureLogSearch NULL	, 'K'	, NULL
			EXEC StoredProcedureLogSearch 1		, 'K'	, NULL
			EXEC StoredProcedureLogSearch 1		, NULL	, NULL
			EXEC StoredProcedureLogSearch NULL	, NULL	, 'W'

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
Create procedure dbo.StoredProcedureLogSearch
(
		@StoredProcedureLogId	INT				= NULL 	OUTPUT						
	,	@Name					VARCHAR(50)											
	,	@AuditId				INT													
	,	@AuditDate				DATETIME		= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'StoredProcedureLog'
)
AS
BEGIN
	-- if the client did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@Name = '%'
		END

	SELECT	a.StoredProcedureLogId					
		,	a.Name									
		,	a.TimeOfExecution						
		,	a.ExecutedBy			
	FROM	dbo.StoredProcedureLog a	
	WHERE	a.Name LIKE @Name	+ '%'	
	AND a.StoredProcedureLogId		  = ISNULL(@StoredProcedureLogId, a.StoredProcedureLogId)
	ORDER BY a.Name		ASC,
			 a.StoredProcedureLogId	ASC	

END
GO
	

