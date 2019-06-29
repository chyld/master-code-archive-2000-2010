<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Movies.ascx.cs" Inherits="Cingulariti.ANTLIA.Presence.Controls.Movies" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:ScriptManagerProxy ID="scriptmanagerproxy" runat="server" />

<asp:UpdatePanel ID="updatepanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    
        <table cellpadding="0" cellspacing="0" class="moviecontrol">
        <!-- +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+= -->
        <tr>
        <td colspan="2" class="movietitle">
        <!-- ================================ -->
            Box Office Analytics :: <asp:Label ID="labelDate" runat="server" />
        <!-- ================================ -->
        </td>
        </tr>
        <!-- +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+= -->
        <tr>
        <td>
        <!-- ================================ -->
            <asp:Chart ID="chartTotalGross" runat="server" Palette="Fire" BackColor="#FFFFFF" Height="350px" Width="350px"
                       ImageLocation="~/Temp/ChartPic_#SEQ(300,3)" ImageStorageMode="UseHttpHandler" ImageType="Png">
                <Series>
                    <asp:Series Name="seriesTotalGross" ChartArea="areaTotalGross" XValueType="String" YValueType="Double" ChartType="Pie" 
                                Font="Trebuchet MS, 7pt" CustomProperties="PieDrawingStyle=Concave, PieLabelStyle=Outside"
                                BorderColor="64, 64, 64, 64" Color="180, 65, 140, 240" />
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="areaTotalGross">
                        <Position Width="100" Height="100" />
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        <!-- ================================ -->
        </td>
        <td>
        <!-- ================================ -->
            <asp:Chart ID="chartTheaters" runat="server" Palette="Fire" BackColor="#FFFFFF" Height="350px" Width="350px"
                       ImageLocation="~/Temp/ChartPic_#SEQ(300,3)" ImageStorageMode="UseHttpHandler" ImageType="Png">
                <Series>
                    <asp:Series Name="seriesTheaters" ChartArea="areaTheaters" XValueType="Auto" YValueType="Auto" ChartType="StepLine" MarkerSize="8" BorderWidth="3" ShadowOffset="2" MarkerStyle="Diamond" ShadowColor="Black" IsValueShownAsLabel="true"
                                Font="Trebuchet MS, 7pt" CustomProperties=""
                                BorderColor="180, 26, 59, 105" Color="220, 224, 64, 10" />
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="areaTheaters">
                        <Position Width="100" Height="100" />
                        <InnerPlotPosition Width="100" Height="75" />
                        <AxisX Interval="1" IsLabelAutoFit="false" IsMarginVisible="true" LineColor="#cccccc">
                            <LabelStyle Font="Trebuchet MS, 7pt" Angle="90" />
                            <MajorGrid Enabled="false" />
                            <MajorTickMark LineColor="#cccccc" />
                        </AxisX>
                        <AxisY Enabled="False" IsStartedFromZero="false" />
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        <!-- ================================ -->
        </td>
        </tr>
        <!-- +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+= -->
        <tr>
        <td colspan="2">
        <!-- ================================ -->
            <asp:TextBox ID="textboxSlider" runat="server" AutoPostBack="true" />
            <ajax:SliderExtender ID="sliderextender" runat="server" Length="700" HandleImageUrl="~/Images/blue-slider.png" HandleCssClass="movieslider"
                     BehaviorID="textboxSlider" TargetControlID="textboxSlider" Orientation="Horizontal" EnableHandleAnimation="true" />

        <!-- ================================ -->
        </td>
        </tr>
        <!-- +=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+= -->
        <tr>
        <td>
        <!-- ================================ -->
            <asp:Chart ID="chartAverage" runat="server" Palette="Fire" BackColor="#FFFFFF" Height="350px" Width="350px"
                       ImageLocation="~/Temp/ChartPic_#SEQ(300,3)" ImageStorageMode="UseHttpHandler" ImageType="Png">
                <Series>
                    <asp:Series Name="seriesAverage" ChartArea="areaAverage" XValueType="Auto" YValueType="Auto" ChartType="Column" IsValueShownAsLabel="true" LabelFormat="C"
                                Font="Trebuchet MS, 7pt" CustomProperties="DrawingStyle=Emboss"
                                BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240" />
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="areaAverage">
                        <Position Width="100" Height="100" />
                        <InnerPlotPosition Width="100" Height="75" />
                        <AxisX Interval="1" IsLabelAutoFit="false" IsMarginVisible="true" LineColor="#cccccc">
                            <LabelStyle Font="Trebuchet MS, 7pt" Angle="90" />
                            <MajorGrid Enabled="false" />
                            <MajorTickMark LineColor="#cccccc" />
                        </AxisX>
                        <AxisY Enabled="False" IsStartedFromZero="false" />
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        <!-- ================================ -->
        </td>
        <td>
        <!-- ================================ -->
            <asp:Chart ID="chartCombo" runat="server" Palette="Fire" BackColor="#FFFFFF" Height="350px" Width="350px"
                       ImageLocation="~/Temp/ChartPic_#SEQ(300,3)" ImageStorageMode="UseHttpHandler" ImageType="Png">
                <Series>
                    <asp:Series Name="seriesDailyGross" ChartArea="areaDailyGross" XValueType="Auto" YValueType="Auto" ChartType="SplineArea" IsValueShownAsLabel="true" LabelFormat="C"
                                Font="Trebuchet MS, 7pt" CustomProperties=""
                                BorderColor="#252525" Color="#ffff00" />
                    <asp:Series Name="seriesPercentChange" ChartArea="areaPercentChange" XValueType="Auto" YValueType="Auto" ChartType="Point" IsValueShownAsLabel="true" LabelFormat="P" MarkerSize="20" MarkerStyle="Star5"
                                Font="Trebuchet MS, 7pt" CustomProperties=""
                                BorderColor="#252525" Color="#ff0033" />
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="areaDailyGross" BackColor="Transparent">
                        <Position Width="100" Height="100" />
                        <InnerPlotPosition Width="100" Height="75" />
                        <AxisY Enabled="False" ></AxisY>
                        <AxisX Enabled="False" ></AxisX>
                    </asp:ChartArea>
                    <asp:ChartArea Name="areaPercentChange" BackColor="Transparent">
                        <Position Width="100" Height="100" />
                        <InnerPlotPosition Width="100" Height="75" />
                        <AxisY Enabled="False" ></AxisY>
                        <AxisX Interval="1" IsLabelAutoFit="false" IsMarginVisible="true" LineColor="#cccccc">
                            <LabelStyle Font="Trebuchet MS, 7pt" Angle="90" />
                            <MajorGrid Enabled="false" />
                            <MajorTickMark LineColor="#cccccc" />
                        </AxisX>
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        <!-- ================================ -->
        </td>
        </tr>
        <!-- ================================ -->
        </table>

        <div class="moviecopyright">
            Copyright &copy; Cingulariti
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
