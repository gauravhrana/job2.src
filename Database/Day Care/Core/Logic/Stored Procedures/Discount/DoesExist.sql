IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='DiscountDoesExist')
BEGIN
	PRINT 'Dropping Procedure DiscountDoesExist'
	DROP  Procedure  DiscountDoesExist
END
GO

PRINT 'Creating Procedure DiscountDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: DiscountDoesExist
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

Create procedure dbo.DiscountDoesExist
(
		@Name					VARCHAR(50)		= NULL
	,	@ApplicationId			INT			
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'Discount'		
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.Discount a
	WHERE		a.Name = @Name
	AND			a.ApplicationId	= @ApplicationId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

