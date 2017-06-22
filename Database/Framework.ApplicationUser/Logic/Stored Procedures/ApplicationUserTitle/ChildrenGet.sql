--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserTitleChildrenGet')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserTitleChildrenGet'
--	DROP  Procedure ApplicationUserTitleChildrenGet
--END
--GO

--PRINT 'Creating Procedure ApplicationUserTitleChildrenGet'
--GO


/******************************************************************************
**		File: 
**		Name: ApplicationUserTitleChildrenGet
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.ApplicationUserTitleChildrenGet
(
		@ApplicationUserTitleId		INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME	= NULL   
	,	@SystemEntityType			VARCHAR(50) = 'ApplicationUserTitle'
)
AS
BEGIN

	-- GET ApplicationUser Records
	SELECT	a.ApplicationUserId	
		,	a.ApplicationId										
		,	a.FirstName										
		,	a.LastName	
		,	a.MiddleName
		,	a.ApplicationUserTitleId
		,	b.Name													AS	'ApplicationUserTitle'								
		,	a.FirstName	+ ' ' + a.LastName +	' ' + a.LastName	AS	'FullName'
		,	c.Name													AS	'Application'
	FROM		dbo.ApplicationUser			a
	INNER JOIN  dbo.ApplicationUserTitle	b	ON	a.ApplicationUserTitleId	= b.ApplicationUserTitleId
	INNER JOIN	dbo.Application				c	ON	a.ApplicationId				= c.ApplicationId
	WHERE	a.ApplicationUserTitleId = @ApplicationUserTitleId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationUserTitleId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   