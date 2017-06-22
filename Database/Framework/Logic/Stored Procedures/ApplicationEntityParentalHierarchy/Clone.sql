IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationEntityParentalHierarchyClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationEntityParentalHierarchyClone'
	DROP  Procedure ApplicationEntityParentalHierarchyClone
END
GO

PRINT 'Creating Procedure ApplicationEntityParentalHierarchyClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ApplicationEntityParentalHierarchyClone
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
**		
**********************************************************************************************/

CREATE Procedure dbo.ApplicationEntityParentalHierarchyClone
(
		@ApplicationEntityParentalHierarchyId	INT			= NULL 	OUTPUT
	,	@ApplicationId							INT			= NULL			
	,	@Name									VARCHAR(50)						
	,	@Description							VARCHAR(50)						
	,	@SortOrder								INT								
	,	@AuditId								INT									
	,	@AuditDate								DATETIME	= NULL				
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationEntityParentalHierarchy'
)
AS
BEGIN

	IF @ApplicationEntityParentalHierarchyId IS NULL
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'ApplicationEntityParentalHierarchy', @ApplicationEntityParentalHierarchyId OUTPUT
	END			
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Description		= Description
		,	@SortOrder			= SortOrder				
	FROM	dbo.ApplicationEntityParentalHierarchy
	WHERE	ApplicationEntityParentalHierarchyId	= @ApplicationEntityParentalHierarchyId
	Order By ApplicationEntityParentalHierarchyId

	EXEC dbo.ApplicationEntityParentalHierarchyInsert 
			@ApplicationEntityParentalHierarchyId	=	NULL
		,	@ApplicationId							=	NULL	
		,	@Name									=	@Name
		,	@Description							=	@Description
		,	@SortOrder								=	@SortOrder
		,	@AuditId								=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationEntityParentalHierarchyId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
