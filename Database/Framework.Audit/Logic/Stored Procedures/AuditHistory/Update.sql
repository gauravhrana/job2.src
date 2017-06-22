IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AuditHistoryUpdate')
BEGIN
	PRINT 'Dropping Procedure AuditHistoryUpdate'
	DROP  Procedure  AuditHistoryUpdate
END
GO

PRINT 'Creating Procedure AuditHistoryUpdate'
GO

/******************************************************************************
**		File: 
**		Name: AuditHistoryUpdate
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

CREATE Procedure dbo.AuditHistoryUpdate
(
		--@AuditHistoryId			INT		= NULL,
		@SystemEntityId			INT	
	,	@EntityKey				INT	
	,	@AuditActionId			INT	
	,	@CreatedDate			DATETIME	= NULL
	,	@CreatedByPersonId		INT				
)
AS
BEGIN

	UPDATE		dbo.AuditHistory
	SET			
	--AuditHistoryId	  = @Audithistoryid,
				SystemEntityId    = @Systementityid
		,		EntityKey	      = @Entitykey
		,		AuditActionId     = @Auditactionid
		,		CreatedDate       = @Createddate
		,		CreatedByPersonId = @Createdbypersonid
 
 END
 GO