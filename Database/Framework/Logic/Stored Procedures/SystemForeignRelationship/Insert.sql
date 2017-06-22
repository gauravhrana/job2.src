IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemForeignRelationshipInsert')
BEGIN
	PRINT 'Dropping Procedure SystemForeignRelationshipInsert'
	DROP  Procedure SystemForeignRelationshipInsert
END
GO

PRINT 'Creating Procedure SystemForeignRelationshipInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:SystemForeignRelationshipInsert
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

CREATE Procedure dbo.SystemForeignRelationshipInsert
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
	,	@AuditDate								DATETIME		= NULL				
	,	@SystemEntityType						VARCHAR(50)		= 'SystemForeignRelationship'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SystemForeignRelationshipId OUTPUT, @AuditId
	
	INSERT INTO dbo.SystemForeignRelationship 
	( 
			SystemForeignRelationshipId
		  , ApplicationId
		  , PrimaryDatabaseId
		  , PrimaryEntityId
		  , ForeignDatabaseId
		  , ForeignEntityId	
		  , FieldName	
		  , SystemForeignRelationshipTypeId				
	)
	VALUES 
	(  
			@SystemForeignRelationshipId	
		,	@ApplicationId
		,   @PrimaryDatabaseId	
		,	@PrimaryEntityId						
		,	@ForeignDatabase				
		,	@ForeignDatabaseId
		,	@FieldName	
		,	@SystemForeignRelationshipTypeId		
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SystemForeignRelationshipId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 