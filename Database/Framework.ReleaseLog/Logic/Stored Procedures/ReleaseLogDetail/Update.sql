IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetailUpdate')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailUpdate'
	DROP  Procedure  ReleaseLogDetailUpdate
END
GO

PRINT 'Creating Procedure ReleaseLogDetailUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseLogDetailUpdate
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

CREATE Procedure dbo.ReleaseLogDetailUpdate
(
		@ReleaseLogDetailId				INT 			
	,	@ReleaseLogId					INT	
	,	@ApplicationId					INT = NULL	 			
	,	@ItemNo							INT					
	,	@Description					VARCHAR(500)			
	,	@SortOrder						INT					
	,	@RequestedBy					VARCHAR(50)						
	,	@PrimaryDeveloper				VARCHAR(50)						
	,	@RequestedDate					DATETIME		
	,	@ReleaseIssueTypeId				INT					
	,	@ReleasePublishCategoryId		INT					
	,	@JIRA							VARCHAR(50)			
	,	@Feature						VARCHAR(255)		
	,	@Module							VARCHAR(255)
	,	@ReleaseFeatureId				INT		
	,	@SystemEntityTypeId				INT				=	NULL		
	,	@PrimaryEntity					VARCHAR(225)		
	,	@TimeSpent						VARCHAR(50)		
	,	@AuditId						INT					
	,	@AuditDate						DATETIME	= NULL	
	,	@SystemEntityType				VARCHAR(50)	= 'ReleaseLogDetail'
)
AS
BEGIN 

	SET ANSI_NULLS ON;

	declare @ModuleId_new INT 	
	
	Set @ModuleId_new = (Select ModuleId from Module where Name = @Module AND ApplicationId = @ApplicationId)
	
	IF(	@ModuleId_new IS NULL) 	
	BEGIN
		
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'Module', @ModuleId_new OUTPUT, @AuditId	
		EXEC ModuleInsert @ModuleId_new,@ApplicationId,@Module,@Module,1,@AuditId 
		UPDATE Configuration.dbo.SystemEntityType SET NextValue= @ModuleId_new+1 WHERE EntityName='Module'	
	
	END
	declare @ModuleId int

	Set @ModuleId = (Select ModuleId from Module where Name = @Module AND ApplicationId = @ApplicationId)	
	
	UPDATE	dbo.ReleaseLogDetail 
	SET		ReleaseLogId					= @ReleaseLogId	
		,	ApplicationId 					= @ApplicationId					
	    ,   ItemNo							= @ItemNo				
		,	Description						= @Description						
		,	SortOrder						= @SortOrder			
		,	PrimaryDeveloper				= @PrimaryDeveloper		
		,	ReleaseIssueTypeId				= @ReleaseIssueTypeId			
		,	ReleasePublishCategoryId		= @ReleasePublishCategoryId	
		,	JIRA							= @JIRA	
		,	ModuleId						= @ModuleId		
		,	ReleaseFeatureId				= @ReleaseFeatureId
		,	Feature							= @Feature					
		,	PrimaryEntity					= @PrimaryEntity
		,	SystemEntityTypeId				= @SystemEntityTypeId
		,	TimeSpent						= ISNULL(@TimeSpent, 'Unknown')
	WHERE	ReleaseLogDetailId		= @ReleaseLogDetailId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ReleaseLogDetailId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO