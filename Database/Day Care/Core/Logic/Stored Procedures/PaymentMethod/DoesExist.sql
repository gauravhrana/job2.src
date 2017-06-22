IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='PaymentMethodDoesExist')
BEGIN
	PRINT 'Dropping Procedure PaymentMethodDoesExist'
	DROP  Procedure  PaymentMethodDoesExist
END
GO

PRINT 'Creating Procedure PaymentMethodDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: PaymentMethodDoesExist
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

Create procedure dbo.PaymentMethodDoesExist
(
		@ApplicationId			INT						
	,	@Name					VARCHAR(50)		= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'PaymentMethod'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.PaymentMethod a
	WHERE		a.Name = @Name	
	AND			a.ApplicationId= @ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'PaymentMethod'	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

