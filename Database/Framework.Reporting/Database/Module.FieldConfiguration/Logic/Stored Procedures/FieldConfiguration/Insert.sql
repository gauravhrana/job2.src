IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationInsert')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationInsert'
	DROP  Procedure FieldConfigurationInsert
END
GO

PRINT 'Creating Procedure FieldConfigurationInsert'
GO
/*********************************************************************************************
**		File: 
**		Name:FieldConfigurationInsert
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

CREATE Procedure dbo.FieldConfigurationInsert
(
		@FieldConfigurationId			INT			= NULL 	OUTPUT		
	,	@Name							VARCHAR(50)						
	,	@Value					        VARCHAR(50)
	,	@HorizontalAlignment			VARCHAR(50)						
	,	@SystemEntityTypeId				INT
	,	@Width							NUMERIC(7,2)					
	,	@Formatting					    VARCHAR(50)						
	,	@ControlType                    VARCHAR(50)	
	,	@ApplicationId					INT	
	,	@GridViewPriority				INT
	,	@DetailsViewPriority			INT	
	,	@FieldConfigurationModeId		INT	
	,	@DisplayColumn					INT		
	,	@CellCount						INT			
	,	@AuditId						INT									
	,	@AuditDate						DATETIME	= NULL				
	,	@SystemEntityType				VARCHAR(50) = 'FieldConfiguration'			
)
AS
BEGIN

	
	INSERT INTO dbo.FieldConfiguration 
	( 
			Name									
		,	Value									
		,	SystemEntityTypeId						
		,	Width									
		,	Formatting								
		,	ControlType		
		,	HorizontalAlignment	
		,	ApplicationId	
		,	GridViewPriority
		,	DetailsViewPriority	
		,	FieldConfigurationModeId	
		,	DisplayColumn	
		,	CellCount
	)
	VALUES 
	(  
			@Name			                        
		,	@Value									
		,	@SystemEntityTypeId						
		,	@Width									
		,	@Formatting								
		,	@ControlType
		,	@HorizontalAlignment	
		,	@ApplicationId	
		,	@GridViewPriority
		,	@DetailsViewPriority	
		,	@FieldConfigurationModeId
		,	@DisplayColumn		
		,	@CellCount			
	)

	SET @FieldConfigurationId = SCOPE_IDENTITY()

	SELECT @FieldConfigurationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	

GO

