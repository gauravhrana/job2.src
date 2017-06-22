IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'ConnectionStringList')
BEGIN
	PRINT 'Dropping Procedure ConnectionStringList'
	DROP  Procedure  dbo.ConnectionStringList
END
GO

PRINT 'Creating Procedure ConnectionStringList'
GO

/******************************************************************************
**		File: 
**		Name: ConnectionStringList
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

CREATE Procedure dbo.ConnectionStringList
(
		@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ConnectionString'
)
AS
BEGIN

	SELECT	a.ConnectionStringId						
		,	a.Name				
		,	a.Description	
		,	a.DataSource	
		,	a.InitialCatalog
		,	a.UserName
		,	a.Password
		,	a.ProviderName
	FROM	 dbo.ConnectionString	a 
	ORDER BY ConnectionStringId		ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END		
GO