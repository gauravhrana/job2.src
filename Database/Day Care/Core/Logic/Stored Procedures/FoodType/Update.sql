IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FoodTypeUpdate')
BEGIN
	PRINT 'Dropping Procedure FoodTypeUpdate'
	DROP  Procedure  FoodTypeUpdate
END
GO

PRINT 'Creating Procedure FoodTypeUpdate'

GO

/******************************************************************************
**		File: 
**		Name: FoodTypeUpdate
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

CREATE Procedure dbo.FoodTypeUpdate
(        
	    @FoodTypeId 		    INT		
	,	@Name				    VARCHAR(50)
	,	@Description		    VARCHAR(500)	= NULL
	,	@SortOrder			    INT				= 1
	,   @AuditId		        INT			
    ,   @AuditDate		        DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'FoodType'
)
AS
BEGIN

    UPDATE	dbo.FoodType
	SET		Name					= @Name			
		,	Description				= @Description  
		,	SortOrder				= @SortOrder
	WHERE	FoodTypeId				= @FoodTypeId
		
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FoodTypeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO

