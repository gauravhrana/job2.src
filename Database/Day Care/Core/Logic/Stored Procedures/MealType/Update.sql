IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MealTypeUpdate')
BEGIN
	PRINT 'Dropping Procedure MealTypeUpdate'
	DROP  Procedure  MealTypeUpdate
END
GO

PRINT 'Creating Procedure MealTypeUpdate'
GO

/******************************************************************************
**		File: 
**		Name: MealTypeUpdate
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
**		----------						-----------
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

CREATE Procedure dbo.MealTypeUpdate
(         
		@MealTypeId		    INT	
	,	@Name				VARCHAR(50)
	,	@Description		VARCHAR(500)	= NULL
	,	@SortOrder			INT				= 1
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME		= NULL
	,	@SystemEntityType	VARCHAR(50)		= 'MealType'	
	 
)
AS
 BEGIN
	UPDATE	dbo.MealType
	SET		Name					    = @Name			
		,	Description                 = @Description  
		,	SortOrder					= @SortOrder
	WHERE	MealTypeId		     		= @MealTypeId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @MealTypeId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END
GO

