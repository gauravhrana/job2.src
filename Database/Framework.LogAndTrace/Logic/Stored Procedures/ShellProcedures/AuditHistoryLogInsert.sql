IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'AuditHistoryInsert')
BEGIN
	PRINT 'Dropping Procedure AuditHistoryInsert'
	DROP  Procedure AuditHistoryInsert
END
GO

PRINT 'Creating Procedure AuditHistoryInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:AuditHistoryInsert
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/
CREATE Procedure dbo.AuditHistoryInsert
(
		@AuditHistoryId			INT			= NULL		OUTPUT
	,	@SystemEntityType		VARCHAR(50) 
	,	@EntityKey				INT			
	,	@AuditAction			VARCHAR(50) 
	,	@CreatedDate			DATETIME	= NULL
	,	@CreatedByPersonId		INT									
)
AS
BEGIN

EXEC CommonServices.dbo.AuditHistoryInsert
			@EntityKey				= @EntityKey
		,	@AuditAction			= @AuditAction
		,	@SystemEntityType		= @SystemEntityType
		,	@CreatedDate			= @CreatedDate
		,	@CreatedByPersonId		= @CreatedByPersonId

END	
GO

 