IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'HelpPageDetails')
BEGIN
  PRINT 'Dropping Procedure HelpPageDetails'
  DROP  Procedure HelpPageDetails
END

GO

PRINT 'Creating Procedure HelpPageDetails'
GO


/******************************************************************************
**		File: 
**		Name: HelpPageDetails
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
**		Date:		Author:				Content:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.HelpPageDetails
(
		@HelpPageId					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'HelpPage'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@HelpPageId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	a.HelpPageId			
		,	a.ApplicationId
		,	a.Name						
		,	a.Content			
		,	a.SortOrder	
		,	a.SystemEntityTypeId	
		,	a.HelpPageContextId
		,	b.EntityName			AS	'SystemEntityType'
		,	c.Name					AS	'HelpPageContext'
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'		
	FROM		dbo.HelpPage						a
	INNER JOIN	Configuration.dbo.SystemEntityType	b	ON	a.SystemEntityTypeId	=	b.SystemEntityTypeId
	INNER JOIN	dbo.HelpPageContext					c	ON	a.HelpPageContextId		=	c.HelpPageContextId
	WHERE		a.HelpPageId = @HelpPageId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @HelpPageId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO
