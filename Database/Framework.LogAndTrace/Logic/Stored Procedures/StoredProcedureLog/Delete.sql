IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogDelete')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogDelete'
	DROP  Procedure StoredProcedureLogDelete
END
GO

PRINT 'Creating Procedure StoredProcedureLogDelete'
GO
/******************************************************************************
**		File: 
**		Name: StoredProcedureLogDelete
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------							-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.StoredProcedureLogDelete
(
		@StoredProcedureLogId 		INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'StoredProcedureLog'
)
AS
BEGIN

	DELETE	 dbo.StoredProcedureLog
	WHERE	 StoredProcedureLogId = @StoredProcedureLogId

	
END
GO
