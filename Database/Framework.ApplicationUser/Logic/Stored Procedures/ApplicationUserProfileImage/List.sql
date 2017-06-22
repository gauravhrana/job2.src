--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserProfileImageList')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserProfileImageList'
--	DROP  Procedure  dbo.ApplicationUserProfileImageList
--END
--GO

--PRINT 'Creating Procedure ApplicationUserProfileImageList'
--GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserProfileImageList
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

CREATE Procedure dbo.ApplicationUserProfileImageList
(
		@ApplicationId			INT
	,	@AuditId				INT			= NULL		
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationUserProfileImage'
)
AS
BEGIN

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
	Where	a.ApplicationId			=	@ApplicationId
	ORDER BY	a.ApplicationUserProfileImageId		ASC
		,		a.ApplicationUserId					ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO