IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRelationDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationRelationDelete'
	DROP  Procedure ApplicationRelationDelete
END
GO

PRINT 'Creating Procedure ApplicationRelationDelete'
GO
/******************************************************************************
**		File: 
**		Name: ApplicationRelationDelete
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
CREATE Procedure dbo.ApplicationRelationDelete
(
		@ApplicationRelationId				INT			
	,	@AuditId							INT					
	,	@AuditDate							DATETIME	= NULL
	,	@SystemEntityType					VARCHAR(50)	= 'ApplicationRelation'
)
AS
BEGIN

	DELETE	dbo.ApplicationRelation
	WHERE	ApplicationRelationId	=		@ApplicationRelationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationRelationId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
