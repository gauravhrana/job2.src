IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PaymentMethodList')
BEGIN
	PRINT 'Dropping Procedure PaymentMethodList'
	DROP PROCEDURE PaymentMethodList
END
GO

PRINT 'Creating Procedure PaymentMethodList'
GO

/******************************************************************************
**		File: 
**		Name: PaymentMethodList
**		Desc: 
**
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**		----------						-----------
**
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:			Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.PaymentMethodList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'PaymentMethod'
)
AS
BEGIN
		SELECT	PaymentMethodId	
			,	ApplicationId   
			,	Name		  	
			,	Description	   
			,	SortOrder
		FROM	dbo.PaymentMethod 
		WHERE ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY PaymentMethodId	ASC
			,	 SortOrder			ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
