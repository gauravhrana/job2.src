IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'EntityOwnerUpdate')
BEGIN
	PRINT 'Dropping Procedure EntityOwnerUpdate'
	DROP  Procedure  EntityOwnerUpdate
END
GO

PRINT 'Creating Procedure EntityOwnerUpdate'
GO

/******************************************************************************
**		File: 
**		Name: EntityOwnerUpdate
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Developer:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.EntityOwnerUpdate
(
		@EntityOwnerId				INT			
	,	@EntityId					INT
	,	@DeveloperRoleId			INT					
	,	@Developer					VARCHAR(50)						
	,	@FeatureOwnerStatusId		INT							
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'EntityOwner'
)
AS
BEGIN

	UPDATE	dbo.EntityOwner 
	SET		EntityId				=	@EntityId				
		,	Developer				=	@Developer	
		,	DeveloperRoleId			=	@DeveloperRoleId			
		,	FeatureOwnerStatusId	=	@FeatureOwnerStatusId							
	WHERE	EntityOwnerId			=	@EntityOwnerId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'EntityOwner'
		,	@EntityKey				= @EntityOwnerId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO