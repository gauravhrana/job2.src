IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = '[ModuleOwnerViewSearch]')
BEGIN
	PRINT 'Dropping Procedure [ModuleOwnerViewSearch]'
	DROP  Procedure  dbo.[ModuleOwnerViewSearch]
END
GO

PRINT 'Creating Procedure [ModuleOwnerViewSearch]'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseLogList
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
**     ----------					   ---------
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

CREATE procedure [dbo].[ModuleOwnerViewSearch]
(
	   	@ModuleOwnerId				INT				= NULL 	
	,	@ApplicationId				INT				= NULL		
	,	@ModuleId					INT				= NULL
	,	@DeveloperRoleId			INT				= NULL	
	,	@Developer					VARCHAR(50)		= NULL				
	,	@FeatureOwnerStatusId		INT				= NULL	
	,	@AuditId					INT								
	,	@AuditDate					DATETIME		= NULL			
	,	@SystemEntityType			VARCHAR(50)		= 'ModuleOwner'
)
AS
BEGIN  
	
	SELECT DISTINCT a.Developer 
	
	FROM 	dbo.ModuleOwner a  	
   
   	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the ModuleOwner did not provide any values
	-- assume search on all possiblities ('%')
	SET @Developer	= ISNULL(@Developer, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Developer))) = 0
		BEGIN
			SET	@Developer = '%'
		END
	
	SELECT	a.ModuleOwnerId			
		,	a.ApplicationId
		,	a.ModuleId
		,	a.DeveloperRoleId						
		,	a.Developer			
		,	a.FeatureOwnerStatusId	
		,	a.TotalHoursWorked
		,	b.Name					AS	'Module'
		,	c.Name					AS	'DeveloperRole'
		,	d.Name					AS	'FeatureOwnerStatus'
	
	FROM		dbo.ModuleOwner		a
	INNER JOIN	dbo.Module			b
		ON	a.ModuleId			=	b.ModuleId
	INNER JOIN	dbo.DeveloperRole	c
		ON	a.DeveloperRoleId	=	c.DeveloperRoleId
	INNER JOIN	dbo.FeatureOwnerStatus	d
		ON	a.FeatureOwnerStatusId	=	d.FeatureOwnerStatusId
	WHERE	a.Developer LIKE @Developer	
	AND		a.ModuleId				= ISNULL(@ModuleId, a.ModuleId )
	AND		a.FeatureOwnerStatusId	= ISNULL(@FeatureOwnerStatusId, a.FeatureOwnerStatusId )
	AND		a.DeveloperRoleId		= ISNULL(@DeveloperRoleId, a.DeveloperRoleId )
	AND		a.ModuleOwnerId			= ISNULL(@ModuleOwnerId, a.ModuleOwnerId )
	AND		a.ApplicationId			= ISNULL(@ApplicationId, a.ApplicationId )
	
	ORDER BY a.ModuleOwnerId	ASC
   
 	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType 		= @SystemEntityType
		,	@EntityKey				= @ModuleOwnerId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO

