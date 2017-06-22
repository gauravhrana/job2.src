IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogDetailDelete')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogDetailDelete'
	DROP  Procedure StoredProcedureLogDetailDelete
END
GO

PRINT 'Creating Procedure StoredProcedureLogDetailDelete'
GO
/******************************************************************************
**		File: 
**		Name: StoredProcedureLogDetailDelete
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
CREATE Procedure dbo.StoredProcedureLogDetailDelete
(
		@StoredProcedureLogDetailId 	INT						
	,	@AuditId						INT						
	,	@AuditDate						DATETIME	= NULL		
	,	@SystemEntityType				VARCHAR(50)	= 'StoredProcedureLogDetail'
)
AS
BEGIN

	DELETE	 dbo.StoredProcedureLogDetail
	WHERE	 StoredProcedureLogDetailId = @StoredProcedureLogDetailId

	
END
GO
