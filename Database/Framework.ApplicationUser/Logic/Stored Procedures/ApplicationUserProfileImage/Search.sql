--IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationUserProfileImageSearch')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserProfileImageSearch'
--	DROP Procedure ApplicationUserProfileImageSearch
--END
--GO

--PRINT 'Creating Procedure ApplicationUserProfileImageSearch'
--GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserProfileImageSearch
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
			EXEC ApplicationUserProfileImageSearch NULL	, NULL	, NULL
			EXEC ApplicationUserProfileImageSearch NULL	, 'K'	, NULL
			EXEC ApplicationUserProfileImageSearch 1	, 'K'	, NULL
			EXEC ApplicationUserProfileImageSearch 1	, NULL	, NULL
			EXEC ApplicationUserProfileImageSearch NULL	, NULL	, 'W'

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
Create procedure ApplicationUserProfileImageSearch
(
		@ApplicationUserProfileImageId			INT				= NULL	
	,	@ApplicationId							INT				= NULL
	,	@ApplicationUserId						INT				= NULL	
	,	@AuditId								INT				= NULL			
	,	@AuditDate								DATETIME		= NULL
	,	@SystemEntityType						VARCHAR(50)		= 'ApplicationUserProfileImage' 
	,	@ApplicationMode						INT				= NULL		
	,	@AddAuditInfo							INT				= 1
	,	@AddTraceInfo							INT				= 0
	,	@ReturnAuditInfo						INT				= 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON
	SELECT	a.ApplicationUserProfileImageId	
		,	a.ApplicationId
		,	a.ApplicationUserId						
		,	a.Image								
		,	b.ApplicationUserName									AS	'ApplicationUserName'	
		,	b.FirstName	+ ' ' + b.LastName +	' ' + b.LastName	AS	'FullName'
		,	c.Name													AS  'Application'			
	FROM		dbo.ApplicationUserProfileImage	a
	INNER JOIN	dbo.ApplicationUser					b	ON	a.ApplicationUserId	=	b.ApplicationUserId
	INNER JOIN	dbo.Application						c	ON	a.ApplicationId		=	c.ApplicationId
	WHERE	a.ApplicationUserProfileImageId = ISNULL(@ApplicationUserProfileImageId,a.ApplicationUserProfileImageId )
	AND		a.ApplicationUserId				= ISNULL(@ApplicationUserId,a.ApplicationUserId )
	AND		a.ApplicationId					= ISNULL(@ApplicationId, a.ApplicationId )
	ORDER BY a.ApplicationUserProfileImageId	ASC
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationUserProfileImageId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END
END
GO
	

