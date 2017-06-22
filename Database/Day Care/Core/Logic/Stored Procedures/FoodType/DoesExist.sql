IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='FoodTypeDoesExist')
BEGIN
	PRINT 'Dropping Procedure FoodTypeDoesExist'
	DROP  Procedure  FoodTypeDoesExist
END
GO

PRINT 'Creating Procedure FoodTypeDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: FoodTypeDoesExist
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
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.FoodTypeDoesExist
(
		@Name					VARCHAR(50)		= NULL
	,	@ApplicationId			INT				
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'FoodType'		
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.FoodType a
	WHERE		a.Name = @Name
	AND			a.ApplicationId = @ApplicationId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

