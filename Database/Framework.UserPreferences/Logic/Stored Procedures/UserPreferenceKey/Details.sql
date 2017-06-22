IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceKeyDetails')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceKeyDetails'
	DROP  Procedure UserPreferenceKeyDetails
END

GO

PRINT 'Creating Procedure UserPreferenceKeyDetails'
GO


/******************************************************************************
**		File: 
**		Name: UserPreferenceKeyDetails
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
CREATE Procedure dbo.UserPreferenceKeyDetails
(
		@UserPreferenceKeyId	INT				
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'UserPreferenceKey'
)
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@UserPreferenceKeyId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	a.UserPreferenceKeyId
		,	a.ApplicationId		
		,	a.Name					
		,	a.Value			
		,	a.DataTypeId				
		,	a.Description				
		,	a.SortOrder
		,	b.Name				AS 'UserPreferenceDataType'	
		,	@LastUpdatedDate	AS	'UpdatedDate'
		,	@LastUpdatedBy		AS	'UpdatedBy'
		,	@LastAuditAction	AS	'LastAction'								
	FROM	dbo.UserPreferenceKey a
			INNER JOIN dbo.UserPreferenceDataType b ON a.DataTypeId = b.UserPreferenceDataTypeId 
	WHERE	a.UserPreferenceKeyId = @UserPreferenceKeyId	
	
	--Create Audit Record
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceKeyId
		,	@AuditAction			= 'Details' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END		
GO
   