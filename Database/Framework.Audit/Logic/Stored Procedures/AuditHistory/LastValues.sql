IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AuditHistoryLastValues')
BEGIN
	PRINT 'Dropping Procedure AuditHistoryLastValues'
	DROP  Procedure AuditHistoryLastValues
END
GO

PRINT 'Creating Procedure AuditHistoryLastValues'
GO
/******************************************************************************
**		Task: 
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
		@EntityKey				INT
	,	@SystemEntityType		VARCHAR(50)
	,	@LastUpdatedBy			VARCHAR(100)	OUT
	,	@LastUpdatedDate		DATETIME		OUT
	,	@LastAuditAction		VARCHAR(50)		OUT
)
AS
BEGIN

	SET NOCOUNT ON

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId	AS INT
	EXEC	dbo.SystemEntityTypeGetId 
			@SystemEntityType	=	@SystemEntityType
		,	@SystemEntityTypeId =	@SystemEntityTypeId OUTPUT

	SELECT	
	TOP 1		@LastUpdatedDate	=	a.CreatedDate 
			,	@LastAuditAction	=	b.Name								
			,	@LastUpdatedBy		=	c.FirstName + ' ' + c.LastName
	FROM 		dbo.AuditHistory a
	INNER JOIN	dbo.AuditAction		b		ON		a.AuditActionId 	= b.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser  			c		ON		a.CreatedByPersonId = c.ApplicationUserId
	WHERE		a.EntityKey			= @EntityKey
	AND			a.SystemEntityId	= @SystemEntityTypeId
	AND			a.AuditActionId 	IN (1, 2)
	ORDER BY	a.CreatedDate DESC	

END
GO
   