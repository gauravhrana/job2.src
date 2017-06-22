IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetailInsert')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailInsert'
	DROP  Procedure ReleaseLogDetailInsert
END
GO

PRINT 'Creating Procedure ReleaseLogDetailInsert'
GO

/*********************************************************************************************
**		File: 
**		Name: ReleaseLogDetailInsert
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

CREATE Procedure dbo.ReleaseLogDetailInsert
(
		@ReleaseLogDetailId				INT			= NULL 	OUTPUT	
	,	@ApplicationId					INT	
	,	@ReleaseLogId					INT								
	,	@ItemNo					        INT								
	,	@Description					VARCHAR(500)						
	,	@SortOrder						INT								
	,	@RequestedBy					VARCHAR(50)						
	,	@PrimaryDeveloper				VARCHAR(50)		
	,	@ReleaseIssueTypeId				INT					
	,	@ReleasePublishCategoryId		INT					
	,	@JIRA							VARCHAR(50)		
	,	@Module							VARCHAR(50)		
	,	@ReleaseFeatureId				INT
	,	@Feature						VARCHAR(255)		
	,	@PrimaryEntity					VARCHAR(225)
	,	@SystemEntityTypeId				INT				=	NULL
	,	@TimeSpent						VARCHAR(50)											
	,	@AuditId						INT									
	,	@AuditDate						DATETIME	= NULL				
	,	@SystemEntityType				VARCHAR(50)	= 'ReleaseLogDetail'
)
AS
BEGIN

SET ANSI_NULLS ON;

	IF @ReleaseLogDetailId IS NULL OR @ReleaseLogDetailId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReleaseLogDetailId OUTPUT, @AuditId
	END
	
	DECLARE @RequestedDate AS DATETIME	
	SELECT @RequestedDate = a.ReleaseDate FROM ReleaseLog a WHERE ReleaseLogId = @ReleaseLogId
	
	declare @ModuleId_new INT	
	Set @ModuleId_new = (Select ModuleId from Module where Name = @Module AND ApplicationId = @ApplicationId)
	IF(	@ModuleId_new IS NULL) 
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'Module', @ModuleId_new OUTPUT, @AuditId	
		EXEC ModuleInsert @ModuleId_new,@ApplicationId,@Module,@Module,1,@AuditId 
		UPDATE Configuration.dbo.SystemEntityType SET NextValue= @ModuleId_new+1 WHERE EntityName='Module'	
	
	END	
	
	declare @ModuleId INT
	
	Set @ModuleId = (Select ModuleId from Module where Name = @Module AND ApplicationId = @ApplicationId)

	INSERT INTO dbo.ReleaseLogDetail 
	( 
			ReleaseLogDetailId	
		,	ApplicationId							
		,	ReleaseLogId				
		,   ItemNo						
		,	Description					
		,	SortOrder					
		,	RequestedBy				         
		,	PrimaryDeveloper		         
		,	RequestedDate			
		,	ReleaseIssueTypeId		
		,	ReleasePublishCategoryId
		,	JIRA					
		,	Feature	
		,	ModuleId	
		,	ReleaseFeatureId			
		,	SystemEntityTypeId
		,	PrimaryEntity
		,	TimeSpent
	)
	VALUES 
	(  
			@ReleaseLogDetailId	
		,	@ApplicationId			
		,	@ReleaseLogId				
		,	@ItemNo						
		,	@Description				
		,	@SortOrder					
		,	@RequestedBy				           
		,	@PrimaryDeveloper		           
		,	@RequestedDate			
		,	@ReleaseIssueTypeId		
		,	@ReleasePublishCategoryId
		,	@JIRA					
		,	@Feature
		,	@ModuleId	
		,	@ReleaseFeatureId
		,	@SystemEntityTypeId				
		,	@PrimaryEntity	
		,	ISNULL(@TimeSpent, 'Unknown')
	)		

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ReleaseLogDetailId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END	
GO

 