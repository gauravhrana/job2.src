IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PaymentMethodDelete')
BEGIN
	PRINT 'Dropping Procedure PaymentMethodDelete'
	DROP  Procedure  PaymentMethodDelete
END
GO

PRINT 'Creating Procedure PaymentMethodDelete'
GO

/******************************************************************************
**		File: 
**		Name: PaymentMethodDelete
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

CREATE Procedure dbo.PaymentMethodDelete
(
	    @PaymentMethodId	INT	
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	= NULL
	,   @SystemEntityType	VARCHAR(50)	= 'PaymentMethod' 
)
AS
BEGIN
	DELETE	dbo.PaymentMethod
	WHERE	PaymentMethodId = @PaymentMethodId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @PaymentMethodId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

