IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceDataTypeDetails')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceDataTypeDetails'
	DROP  Procedure UserPreferenceDataTypeDetails
END
GO

PRINT 'Creating Procedure UserPreferenceDataTypeDetails'
GO


/******************************************************************************
**		File: 
**		Name: UserPreferenceDataTypeDetails
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

CREATE Procedure dbo.UserPreferenceDataTypeDetails
(
		@UserPreferenceDataTypeId		INT					
	,	@AuditId						INT					
	,	@AuditDate						DATETIME = NULL		
	,	@SystemEntityType				VARCHAR(50) = 'UserPreferenceDataType'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@UserPreferenceDataTypeId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	UserPreferenceDataTypeId
		,	ApplicationId	
		,	Name					
		,	Description		
		,	SortOrder		
		,	@LastUpdatedDate	AS	'UpdatedDate'
		,	@LastUpdatedBy		AS	'UpdatedBy'
		,	@LastAuditAction	AS	'LastAction'		
	FROM	dbo.UserPreferenceDataType 
	WHERE	UserPreferenceDataTypeId = @UserPreferenceDataTypeId	
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceDataTypeId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
