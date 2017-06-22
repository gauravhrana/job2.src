IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuUpdate')
BEGIN
	PRINT 'Dropping Procedure MenuUpdate'
	DROP  Procedure  MenuUpdate
END
GO

PRINT 'Creating Procedure MenuUpdate'
GO

/******************************************************************************
**		File: 
**		Name: MenuUpdate
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
CREATE Procedure dbo.MenuUpdate
(
		@MenuId					INT			
	,	@Name					VARCHAR(50)	
	,	@Value					VARCHAR(50)		
	,	@ParentMenuId			INT		  
	,	@PrimaryDeveloper 		VARCHAR(50)				
	,	@Description			VARCHAR(50)		
	,	@SortOrder				INT		
	,	@IsChecked				INT
	,	@IsVisible				INT			
	,	@NavigateURL			VARCHAR(500)					
	,	@ApplicationModule		VARCHAR(100)
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL
	,	@SystemEntityType		VARCHAR(50)	= 'Menu'
)
AS
BEGIN

	SET NOCOUNT ON
	
	UPDATE	dbo.Menu 
	SET		Name				=	@Name	
		,	Value				=	@Value			
		,	NavigateURL			=	@NavigateURL				
		,	ParentMenuId		=	@ParentMenuId
		,	PrimaryDeveloper	=	@PrimaryDeveloper			
		,	Description			=	@Description		
		,	SortOrder			=	@SortOrder
		,	IsVisible			=	@IsVisible
		,	IsChecked			=	@IsChecked	
		,	ApplicationModule	=	@ApplicationModule			
	WHERE	MenuId				=	@MenuId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MenuId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO