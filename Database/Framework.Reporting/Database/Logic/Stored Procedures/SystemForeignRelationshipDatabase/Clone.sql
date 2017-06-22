IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemForeignRelationshipDatabaseClone')
BEGIN
	PRINT 'Dropping Procedure SystemForeignRelationshipDatabaseClone'
	DROP  Procedure SystemForeignRelationshipDatabaseClone
END
GO

PRINT 'Creating Procedure SystemForeignRelationshipDatabaseClone'
GO

/*********************************************************************************************
**		File: 
**		Name: SystemForeignRelationshipDatabaseClone
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
CREATE Procedure dbo.SystemForeignRelationshipDatabaseClone
(
		@SystemForeignRelationshipDatabaseId		INT			= NULL 	OUTPUT	
	,	@ApplicationId								INT			
	,	@Name										VARCHAR(50)						
	,	@Description								VARCHAR(500)						
	,	@SortOrder									INT								
	,	@AuditId									INT									
	,	@AuditDate									DATETIME	= NULL				
	,	@SystemEntityType							VARCHAR(50)	= 'SystemForeignRelationshipDatabase'
)
AS
BEGIN
		IF @SystemForeignRelationshipDatabaseId IS NULL OR @SystemForeignRelationshipDatabaseId = -999999
		BEGIN
			EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SystemForeignRelationshipDatabaseId OUTPUT
		END						
	
		SELECT	@ApplicationId	= ApplicationId
			,	@Description	= Description
			,	@SortOrder		= SortOrder				
		FROM	dbo.SystemForeignRelationshipDatabase
		WHERE   SystemForeignRelationshipDatabaseId		= @SystemForeignRelationshipDatabaseId

		EXEC dbo.SystemForeignRelationshipDatabaseInsert 
			@SystemForeignRelationshipDatabaseId		=	NULL
		,	@ApplicationId							=	@ApplicationId
		,	@Name									=	@Name
		,	@Description							=	@Description
		,	@SortOrder								=	@SortOrder
		,	@AuditId								=	@AuditId

		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SystemForeignRelationshipDatabase'
		,	@EntityKey				= @SystemForeignRelationshipDatabaseId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

	END	
GO
