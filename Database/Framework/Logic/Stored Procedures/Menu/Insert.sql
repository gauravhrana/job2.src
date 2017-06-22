IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuInsert')
BEGIN
	PRINT 'Dropping Procedure MenuInsert'
	DROP  Procedure MenuInsert
END
GO

PRINT 'Creating Procedure MenuInsert'
GO
/*********************************************************************************************
**		File: 
**		Name:MenuInsert
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.MenuInsert
(
		@MenuId					INT			= NULL OUTPUT
	,	@ApplicationId			INT		
	,	@Name					VARCHAR(50)		
	,	@Value					VARCHAR(50)	
	,	@ParentMenuId			INT		  
	,	@PrimaryDeveloper 		VARCHAR(50)		
	,	@Description			VARCHAR(50)		
	,	@SortOrder				INT		
	,	@IsChecked				INT
	,	@IsVisible				INT			
	,	@NavigateURL			VARCHAR(500) 
	,	@ParentMenuName			VARCHAR(50)	= NULL	
	,	@ApplicationModule		VARCHAR(100)
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL
	,	@SystemEntityType		VARCHAR(50) = 'Menu'

)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @MenuId OUTPUT, @AuditId

	SET NOCOUNT ON

	IF @MenuId IS NULL OR @MenuId = -999999
    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @MenuId OUTPUT, @AuditId

	IF @ParentMenuId IS NULL AND @ParentMenuName IS NOT NULL
	BEGIN
		SELECT 	@ParentMenuId	=	MenuId 
		FROM 	dbo.Menu 
		WHERE 	Name 			= 	@ParentMenuName 
		AND 	ApplicationId	=	@ApplicationId
	END
	
	INSERT INTO dbo.Menu 
	( 
			MenuId
		,	ApplicationId
		,	Name	
		,	Value			
		,	ParentMenuId
		,	PrimaryDeveloper				
		,	Description				
		,	SortOrder	
		,	IsChecked	
		,	IsVisible				
		,	NavigateURL		
		,	ApplicationModule		
	)
	VALUES 
	(  
			@MenuId
		,	@ApplicationId	
		,	@Name
		,	@Value					
		,	@ParentMenuId
		,	@PrimaryDeveloper				
		,	@Description			
		,	@SortOrder	
		,	@IsChecked	
		,	@IsVisible				
		,	@NavigateURL	
		,	@ApplicationModule		
	)

	SELECT @MenuId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MenuId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END

GO

