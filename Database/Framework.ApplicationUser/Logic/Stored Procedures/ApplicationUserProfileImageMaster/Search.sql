--IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationUserProfileImageMasterSearch')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserProfileImageMasterSearch'
--	DROP Procedure ApplicationUserProfileImageMasterSearch
--END
--GO

--PRINT 'Creating Procedure ApplicationUserProfileImageMasterSearch'
--GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserProfileImageMasterSearch
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
**		Sample:   
**              
			EXEC ApplicationUserProfileImageMasterSearch NULL	, NULL	, NULL
			EXEC ApplicationUserProfileImageMasterSearch NULL	, 'K'	, NULL
			EXEC ApplicationUserProfileImageMasterSearch 1	, 'K'	, NULL
			EXEC ApplicationUserProfileImageMasterSearch 1	, NULL	, NULL
			EXEC ApplicationUserProfileImageMasterSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
Create procedure ApplicationUserProfileImageMasterSearch
(
		@ApplicationUserProfileImageMasterId	INT				= NULL	
	,	@ApplicationId							INT				= NULL
	,	@Title									VARCHAR(50)		= NULL	
	,	@AuditId								INT				= NULL			
	,	@AuditDate								DATETIME		= NULL
	,	@SystemEntityType						VARCHAR(50)		= 'ApplicationUserProfileImageMaster' 
	,	@ApplicationMode					INT				= NULL		
	,	@AddAuditInfo						INT				 = 1
	,	@AddTraceInfo						INT				 = 0
	,	@ReturnAuditInfo					INT				 = 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON	
	SET	@Title	= ISNULL(@Title, '%')	

	IF LEN(LTRIM(RTRIM(@Title))) = 0 
	BEGIN
		SET	@Title = '%'
	END

	SELECT	a.ApplicationUserProfileImageMasterId	
		,	a.ApplicationId
		,	a.Title						
		,	a.Image		
		,	c.Name													AS  'Application'			
	FROM		dbo.ApplicationUserProfileImageMaster	a	
	INNER JOIN	dbo.Application						c	ON	a.ApplicationId		=	c.ApplicationId
	WHERE	a.ApplicationUserProfileImageMasterId	= ISNULL(@ApplicationUserProfileImageMasterId,a.ApplicationUserProfileImageMasterId )
	AND		a.Title									LIKE @Title	+	'%'
	AND		a.ApplicationId							= ISNULL(@ApplicationId, a.ApplicationId )
	ORDER BY a.ApplicationUserProfileImageMasterId	ASC
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationUserProfileImageMasterId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END
END
GO
	

