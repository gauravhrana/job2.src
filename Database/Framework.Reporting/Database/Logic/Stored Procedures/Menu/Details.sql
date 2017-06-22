IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuDetails')
BEGIN
	PRINT 'Dropping Procedure MenuDetails'
	DROP  Procedure MenuDetails
END

GO

PRINT 'Creating Procedure MenuDetails'
GO


/******************************************************************************
**		File: 
**		Name: MenuDetails
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
CREATE Procedure dbo.MenuDetails
(
		@MenuId					INT				
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'Menu'
)
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@MenuId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	a.MenuId			
		,	a.ApplicationId	
		,	a.Name			
		,	a.Value
		,	a.ParentMenuId		
		,	b.Name			AS 'ParentMenu'	
		,	a.PrimaryDeveloper
		,	a.IsChecked		
		,	a.IsVisible		
		,	a.NavigateURL		
		,	a.Description		
		,	a.SortOrder	
		,	c.Value			AS 'MenuDisplayName'
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'
	FROM	dbo.Menu		a
	LEFT JOIN dbo.Menu		b ON a.ParentMenuId = b.MenuId 
	INNER JOIN	dbo.MenuDisplayName	c
		ON	a.MenuId = c.MenuId
	WHERE	a.MenuId = @MenuId
	AND		c.IsDefault = 1
		
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MenuId
		,	@AuditAction			= 'Details' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END		
GO
   