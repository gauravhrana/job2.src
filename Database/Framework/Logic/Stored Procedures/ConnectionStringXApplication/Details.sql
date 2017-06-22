IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ConnectionStringXApplicationDetails')
BEGIN
	PRINT 'Dropping Procedure ConnectionStringXApplicationDetails'
	DROP  Procedure ConnectionStringXApplicationDetails
END
GO

PRINT 'Creating Procedure ConnectionStringXApplicationDetails'
GO


/******************************************************************************
**		File: 
**		Name: ConnectionStringXApplicationDetails
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

CREATE Procedure dbo.ConnectionStringXApplicationDetails
(
		@ConnectionStringXApplicationId				INT		
	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL	
	,	@SystemEntityType							VARCHAR(50)	= 'ConnectionStringXApplication'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ConnectionStringXApplicationId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	
	
	SELECT	a.ConnectionStringXApplicationId	
		,	a.ApplicationId							
		,	a.ConnectionStringId								
		,	b.Name				AS	'Application'			
		,	c.Name				AS	'ConnectionString'	
		,	@LastUpdatedDate	AS	'UpdatedDate'
		,	@LastUpdatedBy		AS	'UpdatedBy'
		,	@LastAuditAction	AS	'LastAction'					
	FROM		dbo.ConnectionStringXApplication					a
	INNER JOIN	AuthenticationAndAuthorization.dbo.Application		b	ON	a.ApplicationId	=	b.ApplicationId
	INNER JOIN	dbo.ConnectionString								c	ON	a.ConnectionStringId	=	c.ConnectionStringId
	WHERE	a.ConnectionStringXApplicationId	=	ISNULL(@ConnectionStringXApplicationId,	a.ConnectionStringXApplicationId)	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ConnectionStringXApplicationId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   