IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='StoredProcedureLogRawSearch')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogRawSearch'
	DROP Procedure StoredProcedureLogRawSearch
END
GO

PRINT 'Creating Procedure StoredProcedureLogRawSearch'
GO

/******************************************************************************
**		File: 
**		Name: StoredProcedureLogRawSearch
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
			EXEC StoredProcedureLogRawSearch NULL	, NULL	, NULL
			EXEC StoredProcedureLogRawSearch NULL	, 'K'	, NULL
			EXEC StoredProcedureLogRawSearch 1		, 'K'	, NULL
			EXEC StoredProcedureLogRawSearch 1		, NULL	, NULL
			EXEC StoredProcedureLogRawSearch NULL	, NULL	, 'W'

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
Create procedure dbo.StoredProcedureLogRawSearch
(
		@StoredProcedureLogRawId	INT			= NULL 	OUTPUT		
	,	@StoredProcedureLogId		INT			= NULL				
	,	@AuditId					INT								
	,	@AuditDate					DATETIME	= NULL
	,	@SystemEntityType			VARCHAR(50)	= 'StoredProcedureLogRaw'
)
AS
BEGIN
	SELECT	a.StoredProcedureLogRawId		
		,	a.StoredProcedureLogId				
		,	a.InputParameters						
		,	a.InputValues	
	FROM	dbo.StoredProcedureLogRaw a
	WHERE	a.StoredProcedureLogId	  = ISNULL(@StoredProcedureLogId, a.StoredProcedureLogId)	
	AND a.StoredProcedureLogRawId	  = ISNULL(@StoredProcedureLogRawId, a.StoredProcedureLogRawId)	
	ORDER BY a.StoredProcedureLogId	ASC,
			 a.StoredProcedureLogRawId	ASC

END
GO
	

