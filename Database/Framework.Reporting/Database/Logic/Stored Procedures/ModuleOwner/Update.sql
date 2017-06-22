IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ModuleOwnerUpdate')
BEGIN
	PRINT 'Dropping Procedure ModuleOwnerUpdate'
	DROP  Procedure  ModuleOwnerUpdate
END
GO

PRINT 'Creating Procedure ModuleOwnerUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ModuleOwnerUpdate
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

CREATE Procedure dbo.ModuleOwnerUpdate
(
		@ModuleOwnerId				INT			
	,	@ModuleId					INT
	,	@DeveloperRoleId			INT					
	,	@Developer					VARCHAR(50)						
	,	@FeatureOwnerStatusId		INT							
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'ModuleOwner'
)
AS
BEGIN

	UPDATE	dbo.ModuleOwner 
	SET		ModuleId				=	@ModuleId				
		,	Developer				=	@Developer	
		,	DeveloperRoleId			=	@DeveloperRoleId			
		,	FeatureOwnerStatusId	=	@FeatureOwnerStatusId							
	WHERE	ModuleOwnerId			=	@ModuleOwnerId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ModuleOwner'
		,	@EntityKey				= @ModuleOwnerId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO