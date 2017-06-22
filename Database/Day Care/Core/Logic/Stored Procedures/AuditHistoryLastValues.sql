IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AuditHistoryLastValues')
BEGIN
	PRINT 'Dropping Procedure AuditHistoryLastValues'
	DROP  Procedure AuditHistoryLastValues
END
GO

PRINT 'Creating Procedure AuditHistoryLastValues'
GO

/******************************************************************************
**		File: 
**		Name: AuditHistoryLastValues
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

CREATE Procedure dbo.AuditHistoryLastValues
(
		@MealTypeId		    INT
	,   @AuditId			INT			
	,   @AuditDate		    DATETIME	= NULL	
	,	@SystemEntityType	VARCHAR(50)	= 'MealType'
)
AS
BEGIN
	

	EXEC CommonServices.dbo.AuditHistoryLastValues
			@EntityKey				=	@EntityKey
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

END		
GO
   