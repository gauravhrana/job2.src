IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceDataTypeInsert')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceDataTypeInsert'
	DROP  Procedure UserPreferenceDataTypeInsert
END
GO

PRINT 'Creating Procedure UserPreferenceDataTypeInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:UserPreferenceDataTypeInsert
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/

CREATE Procedure dbo.UserPreferenceDataTypeInsert
(
		@UserPreferenceDataTypeId			INT			= NULL 	OUTPUT	
	,	@ApplicationId						INT
	,	@Name								VARCHAR(50)					
	,	@Description						VARCHAR(50)					
	,	@SortOrder							INT							
	,	@AuditId							INT							
	,	@AuditDate							DATETIME	= NULL			
	,	@SystemEntityType					VARCHAR(50) = 'UserPreferenceDataType'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'UserPreferenceDataType', @UserPreferenceDataTypeId OUTPUT
		
	--if blank, then assume search on all possiblities ('%')
	--IF LEN(LTRIM(RTRIM(@UserPreferenceDataTypeId))) = 0 
	--BEGIN
	--	SET	@UserPreferenceDataTypeId = 'NULL'
	--END

	INSERT INTO dbo.UserPreferenceDataType 
	( 
			UserPreferenceDataTypeId
		,	ApplicationId						
		,	Name							
		,	Description						
		,	SortOrder							
	)
	VALUES 
	(  
			@UserPreferenceDataTypeId	
		,	@ApplicationId	
		,	@Name							
		,	@Description					
		,	@SortOrder		
	)

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
	     	@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceDataTypeId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
