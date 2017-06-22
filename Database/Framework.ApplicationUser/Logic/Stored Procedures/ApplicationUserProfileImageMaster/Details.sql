--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserProfileImageMasterDetails')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserProfileImageMasterDetails'
--	DROP  Procedure ApplicationUserProfileImageMasterDetails
--END
--GO

--PRINT 'Creating Procedure ApplicationUserProfileImageMasterDetails'
--GO


/******************************************************************************
**		File: 
**		Name: ApplicationUserProfileImageMasterDetails
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

CREATE Procedure dbo.ApplicationUserProfileImageMasterDetails
(
		@ApplicationUserProfileImageMasterId	INT			= NULL	
	,	@AuditId								INT	        = NULL				
	,	@AuditDate								DATETIME	= NULL	
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationUserProfileImageMaster'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ApplicationUserProfileImageMasterId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	
	
	SELECT	a.ApplicationUserProfileImageMasterId	
		,	a.ApplicationId
		,	a.Title						
		,	a.Image								
		,	c.Name													AS  'Application'		
		,	@LastUpdatedDate										AS	'UpdatedDate'
		,	@LastUpdatedBy											AS	'UpdatedBy'
		,	@LastAuditAction										AS	'LastAction'					
	FROM		dbo.ApplicationUserProfileImageMaster	a
	INNER JOIN	dbo.Application						c	ON	a.ApplicationId		=	c.ApplicationId
	WHERE	a.ApplicationUserProfileImageMasterId	=	ISNULL(@ApplicationUserProfileImageMasterId,	a.ApplicationUserProfileImageMasterId)	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationUserProfileImageMasterId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   