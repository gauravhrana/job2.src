IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationClone')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationClone'
	DROP  Procedure FieldConfigurationClone
END
GO

PRINT 'Creating Procedure FieldConfigurationClone'
GO
/*********************************************************************************************
**		File: 
**		Name: FieldConfigurationClone
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
**		
**********************************************************************************************/

CREATE Procedure dbo.FieldConfigurationClone
(
		@FieldConfigurationId				INT			= NULL 	OUTPUT			
	,	@ApplicationId						INT		
	,	@Name								VARCHAR(50)						
	,	@Value								VARCHAR(50)	
	,	@HorizontalAlignment				VARCHAR(50)						
	,	@SystemEntityTypeId					INT								
	,	@Width								NUMERIC(7,2)					
	,	@Formatting							VARCHAR(50)						
	,	@ControlType						VARCHAR(50)
	,	@GridViewPriority					INT
	,	@DetailsViewPriority				INT	
	,	@FieldConfigurationModeId			INT	
	,	@DisplayColumn						INT	
	,	@CellCount							INT		
	,	@AuditId							INT								
	,	@AuditDate							DATETIME	= NULL				
	,	@SystemEntityType					VARCHAR(50) = 'FieldConfiguration'	
)
AS
BEGIN		
	
	SELECT	Value						=	@Value				
		,	SystemEntityTypeId			=	@SystemEntityTypeId	
		,	Width						=	@Width			    
		,	Formatting					=	@Formatting	        					
		,	ControlType	 				=	@ControlType 
		,	HorizontalAlignment			=	@HorizontalAlignment	
		,	ApplicationId				=	@ApplicationId	
		,	GridViewPriority			=	@GridViewPriority
		,	DetailsViewPriority			=	@DetailsViewPriority		
		,	FieldConfigurationModeId	=	@FieldConfigurationModeId
		,	DisplayColumn				=	@DisplayColumn	
		,	CellCount					=	@CellCount
	FROM	dbo.FieldConfiguration
	WHERE	FieldConfigurationId	= @FieldConfigurationId

	EXEC dbo.FieldConfigurationInsert 
			@FieldConfigurationId			=	NULL				    
		,	@Name							=	@Name				
		,	@Value							=	@Value				
		,	@SystemEntityTypeId				=	@SystemEntityTypeId	
		,	@HorizontalAlignment			=	@HorizontalAlignment
		,	@Width							=	@Width			    
		,	@Formatting						=	@Formatting			        						
		,	@ControlType	 				=	@ControlType 
		,	@ApplicationId					=	@ApplicationId						
		,	@AuditId						=	@AuditId
		,	@GridViewPriority				=	@GridViewPriority
		,	@DetailsViewPriority			=	@DetailsViewPriority
		,	@FieldConfigurationModeId		=	@FieldConfigurationModeId
		,	@DisplayColumn					=	@DisplayColumn
		,	@CellCount						=	@CellCount

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	

GO

