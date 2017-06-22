IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='StoredProcedureLogDetailSearch')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogDetailSearch'
	DROP Procedure StoredProcedureLogDetailSearch
END
GO

PRINT 'Creating Procedure StoredProcedureLogDetailSearch'
GO

/******************************************************************************
**		File: 
**		Name: StoredProcedureLogDetailSearch
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
			EXEC StoredProcedureLogDetailSearch NULL	, NULL	, NULL
			EXEC StoredProcedureLogDetailSearch NULL	, 'K'	, NULL
			EXEC StoredProcedureLogDetailSearch 1		, 'K'	, NULL
			EXEC StoredProcedureLogDetailSearch 1		, NULL	, NULL
			EXEC StoredProcedureLogDetailSearch NULL	, NULL	, 'W'

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
Create procedure dbo.StoredProcedureLogDetailSearch
(
		@StoredProcedureLogDetailId		INT				= NULL 	OUTPUT		
	,	@StoredProcedureLogId			INT				= NULL				
	,	@AuditId						INT									
	,	@AuditDate						DATETIME		= NULL	
	,	@SystemEntityType				VARCHAR(50)		= 'StoredProcedureLogDetail'
)
AS
BEGIN
	SELECT	a.StoredProcedureLogDetailId		
		,	a.StoredProcedureLogId				
		,	a.ParameterName						
		,	a.ParameterValue	
	FROM	dbo.StoredProcedureLogDetail a
	WHERE a.StoredProcedureLogId		= ISNULL(@StoredProcedureLogId, a.StoredProcedureLogId)	
	AND a.StoredProcedureLogDetailId	= ISNULL(@StoredProcedureLogDetailId, a.StoredProcedureLogDetailId)
	ORDER BY a.StoredProcedureLogId	ASC,
			 a.StoredProcedureLogDetailId	ASC

END
GO
	

