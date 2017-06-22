--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserProfileImageMasterList')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserProfileImageMasterList'
--	DROP  Procedure  dbo.ApplicationUserProfileImageMasterList
--END
--GO

--PRINT 'Creating Procedure ApplicationUserProfileImageMasterList'
--GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserProfileImageMasterList
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

CREATE Procedure dbo.ApplicationUserProfileImageMasterList
(
		@ApplicationId			INT
	,	@AuditId				INT			= NULL		
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationUserProfileImageMaster'
)
AS
BEGIN

	SELECT	a.ApplicationUserProfileImageMasterId	
		,	a.ApplicationId
		,	a.Title						
		,	a.Image								
		,	c.Name													AS  'Application'			
	FROM		dbo.ApplicationUserProfileImageMaster	a
	INNER JOIN	dbo.Application						c	ON	a.ApplicationId		=	c.ApplicationId
	Where	a.ApplicationId			=	@ApplicationId
	ORDER BY	a.ApplicationUserProfileImageMasterId		ASC
		,		a.Title					ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO