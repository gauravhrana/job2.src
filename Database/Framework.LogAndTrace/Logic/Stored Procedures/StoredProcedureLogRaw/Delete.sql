IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogRawDelete')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogRawDelete'
	DROP  Procedure StoredProcedureLogRawDelete
END
GO

PRINT 'Creating Procedure StoredProcedureLogRawDelete'
GO
/******************************************************************************
**		File: 
**		Name: StoredProcedureLogRawDelete
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
CREATE Procedure dbo.StoredProcedureLogRawDelete
(
		@StoredProcedureLogRawId 		INT						
	,	@AuditId						INT						
	,	@AuditDate						DATETIME	= NULL		
	,	@SystemEntityType				VARCHAR(50)	= 'StoredProcedureLogRaw'
)
AS
BEGIN

	DELETE	 dbo.StoredProcedureLogRaw
	WHERE	 StoredProcedureLogRawId = @StoredProcedureLogRawId

	
END
GO
