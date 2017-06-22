IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationUpdate')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationUpdate'
	DROP  Procedure FieldConfigurationUpdate
END
GO

PRINT 'Creating Procedure FieldConfigurationUpdate'
GO
/******************************************************************************
**		File: 
**		Name: FieldConfigurationUpdate
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

CREATE Procedure dbo.FieldConfigurationUpdate
(
		@FieldConfigurationId			INT		 						
	,	@Name							VARCHAR(50)						
	,	@Value					        VARCHAR(50)
	,	@SystemEntityTypeId				INT								
	,	@Width							NUMERIC(7,2)					
	,	@Formatting					    VARCHAR(50)						
	,	@ControlType                    VARCHAR(50)
	,	@HorizontalAlignment			VARCHAR(50)			
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

	UPDATE	dbo.FieldConfiguration 
	SET		Name							= @Name									
		,	Value							= @Value								
		,	SystemEntityTypeId				= @SystemEntityTypeId					
		,	Width							= @Width								
		,	Formatting					    = @Formatting							
		,	ControlType						= @ControlType	
		,	HorizontalAlignment				= @HorizontalAlignment
		,	GridViewPriority				= @GridViewPriority
		,	DetailsViewPriority				= @DetailsViewPriority
		,	FieldConfigurationModeId		= @FieldConfigurationModeId	
		,	DisplayColumn					= @DisplayColumn
		,	CellCount						= @CellCount
	WHERE	FieldConfigurationId			= @FieldConfigurationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		

GO

