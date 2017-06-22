IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AuditHistoryDeleteByEntity')
BEGIN
	PRINT 'Dropping Procedure AuditHistoryDeleteByEntity'
	DROP  Procedure AuditHistoryDeleteByEntity
END
GO

PRINT 'Creating Procedure AuditHistoryDeleteByEntity'
GO


/******************************************************************************
**		File: 
**		Name: AuditHistoryDeleteByEntity
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

CREATE Procedure dbo.AuditHistoryDeleteByEntity
(
		@SystemEntityId			INT
	,	@EntityKey				INT	
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'AuditHistory'
)
AS
BEGIN

	DELETE	dbo.AuditHistory
	WHERE	SystemEntityId		=	@SystemEntityId
	AND		EntityKey =
			CASE
				WHEN @EntityKey IS NULL THEN EntityKey
				ELSE @EntityKey
			END		

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @EntityKey
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO


