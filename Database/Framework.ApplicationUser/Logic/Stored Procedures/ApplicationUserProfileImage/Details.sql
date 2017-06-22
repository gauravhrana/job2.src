--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserProfileImageDetails')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserProfileImageDetails'
--	DROP  Procedure ApplicationUserProfileImageDetails
--END
--GO

--PRINT 'Creating Procedure ApplicationUserProfileImageDetails'
--GO


/******************************************************************************
**		File: 
**		Name: ApplicationUserProfileImageDetails
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

CREATE Procedure dbo.ApplicationUserProfileImageDetails
(
		@ApplicationUserProfileImageId		INT			= NULL	
	,	@AuditId							INT	        = NULL				
	,	@AuditDate							DATETIME	= NULL	
	,	@SystemEntityType					VARCHAR(50)	= 'ApplicationUserProfileImage'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ApplicationUserProfileImageId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	
	
	SELECT	a.ApplicationUserProfileImageId	
		,	a.ApplicationId
		,	a.ApplicationUserId						
		,	a.Image								
		,	b.ApplicationUserName									AS	'ApplicationUserName'	
		,	b.FirstName	+ ' ' + b.LastName +	' ' + b.LastName	AS	'FullName'	
		,	c.Name													AS  'Application'		
		,	@LastUpdatedDate										AS	'UpdatedDate'
		,	@LastUpdatedBy											AS	'UpdatedBy'
		,	@LastAuditAction										AS	'LastAction'					
	FROM		dbo.ApplicationUserProfileImage	a
	INNER JOIN	dbo.ApplicationUser					b	ON	a.ApplicationUserId	=	b.ApplicationUserId
	INNER JOIN	dbo.Application						c	ON	a.ApplicationId		=	c.ApplicationId
	WHERE	a.ApplicationUserProfileImageId	=	ISNULL(@ApplicationUserProfileImageId,	a.ApplicationUserProfileImageId)	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationUserProfileImageId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   