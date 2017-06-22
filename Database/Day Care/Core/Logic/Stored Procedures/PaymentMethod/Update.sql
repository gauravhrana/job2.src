IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PaymentMethodUpdate')
BEGIN
	PRINT 'Dropping Procedure PaymentMethodUpdate'
	DROP  Procedure  PaymentMethodUpdate
END
GO

PRINT 'Creating Procedure PaymentMethodUpdate'

GO

/******************************************************************************
**		File: 
**		Name: PaymentMethodUpdate
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

CREATE Procedure dbo.PaymentMethodUpdate
(          
		@PaymentMethodId		INT					
	,	@Name				    VARCHAR(50)
	,	@Description		    VARCHAR(500)	= NULL
	,	@SortOrder			    INT				= 1
	,   @AuditId			    INT			
    ,   @AuditDate		        DATETIME		= NULL
	,	@SystemEntityType	    VARCHAR(50)		= 'PaymentMethod'
	 
)
AS
BEGIN

	UPDATE	dbo.PaymentMethod
	SET		PaymentMethodId             = @PaymentMethodId		  				
		,	Name					    = @Name			      		
		,	Description                 = @Description        
		,	SortOrder					= @SortOrder
	WHERE	PaymentMethodId		     	= @PaymentMethodId	

	 --Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @PaymentMethodId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END
GO

