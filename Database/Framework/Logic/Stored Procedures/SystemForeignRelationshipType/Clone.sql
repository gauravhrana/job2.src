IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemForeignRelationshipTypeClone')
BEGIN
	PRINT 'Dropping Procedure SystemForeignRelationshipTypeClone'
	DROP  Procedure SystemForeignRelationshipTypeClone
END
GO

PRINT 'Creating Procedure SystemForeignRelationshipTypeClone'
GO

/*********************************************************************************************
**		File: 
**		Name: SystemForeignRelationshipTypeClone
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
CREATE Procedure dbo.SystemForeignRelationshipTypeClone
(
		@SystemForeignRelationshipTypeId		INT			= NULL 	OUTPUT	
	,	@ApplicationId							INT			
	,	@Name									VARCHAR(50)						
	,	@Description							VARCHAR(500)						
	,	@SortOrder								INT								
	,	@AuditId								INT									
	,	@AuditDate								DATETIME	= NULL				
	,	@SystemEntityType						VARCHAR(50)	= 'SystemForeignRelationshipType'
)
AS
BEGIN
		IF @SystemForeignRelationshipTypeId IS NULL OR @SystemForeignRelationshipTypeId = -999999
		BEGIN
			EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SystemForeignRelationshipTypeId OUTPUT
		END						
	
		SELECT	@ApplicationId	= ApplicationId
			,	@Description	= Description
			,	@SortOrder		= SortOrder				
		FROM	dbo.SystemForeignRelationshipType
		WHERE   SystemForeignRelationshipTypeId		= @SystemForeignRelationshipTypeId

		EXEC dbo.SystemForeignRelationshipTypeInsert 
			@SystemForeignRelationshipTypeId		=	NULL
		,	@ApplicationId							=	@ApplicationId
		,	@Name									=	@Name
		,	@Description							=	@Description
		,	@SortOrder								=	@SortOrder
		,	@AuditId								=	@AuditId

		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SystemForeignRelationshipType'
		,	@EntityKey				= @SystemForeignRelationshipTypeId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

	END	
GO
