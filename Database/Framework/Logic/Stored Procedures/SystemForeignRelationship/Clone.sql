IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemForeignRelationshipClone')
BEGIN
	PRINT 'Dropping Procedure SystemForeignRelationshipClone'
	DROP  Procedure SystemForeignRelationshipClone
END
GO

PRINT 'Creating Procedure SystemForeignRelationshipClone'
GO

/*********************************************************************************************
**		File: 
**		Name: SystemForeignRelationshipClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.SystemForeignRelationshipClone
(
		@SystemForeignRelationshipId			INT				= NULL 	OUTPUT		
	,	@ApplicationId							INT				
	,	@PrimaryDatabaseId						INT
	,	@PrimaryEntityId						INT
	,	@ForeignDatabaseId						INT
	,	@ForeignEntityId						INT
	,	@FieldName								VARCHAR(50)
	,	@SystemForeignRelationshipTypeId		INT												
	,	@AuditId								INT									
	,	@AuditDate								DATETIME	= NULL				
	,	@SystemEntityType						VARCHAR(50)	= 'SystemForeignRelationship'
)
AS
BEGIN

	IF @SystemForeignRelationshipId IS NULL OR @SystemForeignRelationshipId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SystemForeignRelationshipId OUTPUT
	END						
	
	SELECT	@ApplicationId						=	ApplicationId
		,	@PrimaryDatabaseId					=	PrimaryDatabaseId 
		,	@PrimaryEntityId					=	PrimaryEntityId
		,	@ForeignDatabaseId					=	ForeignDatabaseId	
		,   @ForeignEntityId					=   ForeignEntityId
		,	@FieldName							=	FieldName
		,	@SystemForeignRelationshipTypeId	=	SystemForeignRelationshipTypeId
	FROM	dbo.SystemForeignRelationship
	WHERE   SystemForeignRelationshipId		=	@SystemForeignRelationshipId
	ORDER BY SystemForeignRelationshipId

	EXEC dbo.SystemForeignRelationshipInsert 
			@SystemForeignRelationshipId		=	NULL
		,	@ApplicationId						=	@ApplicationId
		,	@PrimaryDatabaseId					=	@PrimaryDatabaseId
		,   @PrimaryEntityId					=   @PrimaryEntityId
		,	@ForeignDatabaseId					=	@ForeignDatabaseId
		,	@ForeignEntityId					=	@ForeignEntityId
		,	@FieldName							=	@FieldName
		,	@SystemForeignRelationshipTypeId	=	@SystemForeignRelationshipTypeId		
		,	@AuditId							=	@AuditId


	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SystemForeignRelationship'
		,	@EntityKey				= @SystemForeignRelationshipId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
