IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ConnectionStringXApplicationList')
BEGIN
	PRINT 'Dropping Procedure ConnectionStringXApplicationList'
	DROP  Procedure  dbo.ConnectionStringXApplicationList
END
GO

PRINT 'Creating Procedure ConnectionStringXApplicationList'
GO

/******************************************************************************
**		File: 
**		Name: ConnectionStringXApplicationList
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
**     ----------					   ---------
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

CREATE Procedure dbo.ConnectionStringXApplicationList
(
		@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ConnectionStringXApplication'
)
AS
BEGIN

	SELECT	a.ConnectionStringXApplicationId	
		,	a.ApplicationId	
		,	a.ApplicationId						
		,	a.ConnectionStringId								
		,	b.Name		AS	'Application'			
		,	c.Name		AS	'ConnectionString'
	FROM		dbo.ConnectionStringXApplication					a
	INNER JOIN	AuthenticationAndAuthorization.dbo.Application		b	ON	a.ApplicationId	=	b.ApplicationId
	INNER JOIN	dbo.ConnectionString								c	ON	a.ConnectionStringId	=	c.ConnectionStringId
	ORDER BY	a.ConnectionStringXApplicationId		ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO