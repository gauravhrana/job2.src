IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'ConnectionStringDetails')
BEGIN
	PRINT 'Dropping Procedure ConnectionStringDetails'
	DROP  Procedure ConnectionStringDetails
END
GO

PRINT 'Creating Procedure ConnectionStringDetails'
GO


/******************************************************************************
**		File: 
**		Name: ConnectionStringDetails
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

CREATE Procedure dbo.ConnectionStringDetails
(
		@ConnectionStringId			INT				
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL						
	,	@SystemEntityType			VARCHAR(50)		= 'ConnectionString'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ConnectionStringId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	a.ConnectionStringId						
		,	a.Name				
		,	a.Description	
		,	a.DataSource	
		,	a.InitialCatalog
		,	a.UserName
		,	a.Password
		,	a.ProviderName	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'			
	FROM	dbo.ConnectionString a
	WHERE	a.ConnectionStringId = @ConnectionStringId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ConnectionStringId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END		
GO
   