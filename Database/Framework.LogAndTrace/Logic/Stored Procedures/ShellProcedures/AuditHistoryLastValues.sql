 IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AuditHistoryLastValues')
BEGIN
	PRINT 'Dropping Procedure AuditHistoryLastValues'
	DROP  Procedure AuditHistoryLastValues
END
GO

PRINT 'Creating Procedure AuditHistoryLastValues'
GO
/******************************************************************************
**		Task: 
**		Name: AuditHistoryLastValues
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

CREATE Procedure dbo.AuditHistoryLastValues
(				
		@EntityKey				INT
	,	@SystemEntityType		VARCHAR(50)
	,	@LastUpdatedBy			VARCHAR(100)	OUT
	,	@LastUpdatedDate		DATETIME		OUT
	,	@LastAuditAction		VARCHAR(50)		OUT
)
AS
BEGIN

	EXEC CommonServices.dbo.AuditHistoryLastValues
			@EntityKey				=	@EntityKey
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT		

END
GO
   