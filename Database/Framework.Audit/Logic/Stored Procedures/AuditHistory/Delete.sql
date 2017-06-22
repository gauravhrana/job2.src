IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AuditHistoryDelete')
BEGIN
	PRINT 'Dropping Procedure AuditHistoryDelete'
	DROP  Procedure AuditHistoryDelete
END
GO

PRINT 'Creating Procedure AuditHistoryDelete'
GO


/******************************************************************************
**		File: 
**		Name: AuditHistoryDelete
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

CREATE Procedure dbo.AuditHistoryDelete
(
	@AuditHistoryId 	INT
)
AS
BEGIN

	DELETE	 dbo.AuditHistory
	WHERE	 AuditHistoryId = @AuditHistoryId

END
GO
